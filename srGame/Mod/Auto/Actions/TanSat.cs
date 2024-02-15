public class TanSat 
{
    // Biến isTansat chỉ ra trạng thái của tấn sát (đang hoạt động hoặc không)
    public static bool Interval = false;

    // Phương thức tansat() thực hiện logic của tính năng tấn sát
    public static void tansat()
    {
        // Lấy thông tin của nhân vật và mục tiêu mob
        var myChar = Char.myCharz();
        var mobFocus = myChar.mobFocus;

        // Nếu không có mục tiêu mob hoặc mục tiêu mob là chính mình
        if (mobFocus == null || (mobFocus != null && mobFocus.isMobMe))
        {
            // Tìm và di chuyển tới mục tiêu mob gần nhất
            var vMob = GameScr.vMob;
            for (int k = 0; k < vMob.size(); k++)
            {
                Mob mob = (Mob)vMob.elementAt(k);
                if (mob.status != 0 && mob.status != 1 && mob.hp > 0 && !mob.isMobMe)
                {
                    myChar.cx = mob.x;
                    myChar.cy = mob.y;
                    myChar.mobFocus = mob;
                    Service.gI().charMove();
                    break;
                }
            }
        }
        // Nếu mục tiêu mob đã chết hoặc không còn tồn tại
        else if (mobFocus.hp <= 0 || mobFocus.status == 1 || mobFocus.status == 0)
        {
            myChar.mobFocus = null;
        }

        // Tìm kỹ năng tốt nhất để sử dụng và thực hiện sử dụng kỹ năng đó
        Skill skill = FindBestSkill();
        if (skill != null)
        {
            GameScr.gI().doSelectSkill(skill, isShortcut: true);
            GameScr.gI().doDoubleClickToObj(myChar.mobFocus);
        }
    }

    // Phương thức FindBestSkill() tìm kỹ năng tốt nhất để sử dụng
    private static Skill FindBestSkill()
    {
        Skill bestSkill = null;
        var onScreenSkills = GameCanvas.isTouch ? GameScr.onScreenSkill : GameScr.keySkill;

        foreach (Skill s in onScreenSkills)
        {
            if (s == null || s.paintCanNotUseSkill || IsInvalidSkillId(s.template.id) || IsInvalidSkillType(s.template.manaUseType))
                continue;

            int manaUse = GetEffectiveManaUse(s);
            if (Char.myCharz().cMP >= manaUse)
            {
                if (bestSkill == null || bestSkill.coolDown < s.coolDown)
                {
                    bestSkill = s;
                }
            }
        }

        return bestSkill;
    }

    // Phương thức IsInvalidSkillId() kiểm tra xem id của kỹ năng có hợp lệ không
    private static bool IsInvalidSkillId(int id)
    {
        return id == 10 || id == 11 || id == 14 || id == 23 || id == 7;
    }

    // Phương thức IsInvalidSkillType() kiểm tra xem loại sử dụng mana của kỹ năng có hợp lệ không
    private static bool IsInvalidSkillType(int type)
    {
        return type != 0 && type != 1 && type != 2;
    }

    // Phương thức GetEffectiveManaUse() tính toán lượng mana thực sự cần sử dụng cho kỹ năng
    private static int GetEffectiveManaUse(Skill skill)
    {
        return skill.template.manaUseType switch
        {
            2 => 1,
            1 => skill.manaUse * Char.myCharz().cMPFull / 100,
            _ => skill.manaUse
        };
    }
}
