using System.Collections;

namespace Mod.Auto.Actions
{
    // Lớp AutoAttack thực thi ThreadActionUpdate để thực hiện việc tự động tấn công
    public class AutoAttack : ThreadActionUpdate<AutoAttack>
    {
        // Phương thức Interval được ghi đè để trả về khoảng thời gian giữa các lần cập nhật (123 miligiây)
        public override int Interval => 123;

        // Phương thức update được ghi đè để thực hiện logic tự động tấn công
        protected override void Update()
        {
            // Lấy thông tin của nhân vật của người chơi
            var myChar = Char.myCharz();
            var mobFocus = myChar.mobFocus;
            var charFocus = myChar.charFocus;

            // Kiểm tra xem có mục tiêu mob hoặc nhân vật nào đang được chọn không
            if (mobFocus != null || charFocus != null)
            {
                // Tạo vector chứa mục tiêu mob và nhân vật
                var vMob = new MyVector(mobFocus != null ? new ArrayList { mobFocus } : null);
                var vChar = new MyVector(charFocus != null ? new ArrayList { charFocus } : null);

                // Kiểm tra xem có thể sử dụng kỹ năng tấn công không
                if (CanUseSkill(myChar))
                {
                    // Gửi yêu cầu tấn công đến server với các tham số tương ứng
                    Service.gI().sendPlayerAttack(vMob, vChar, -1); // type = -1 -> tự động
                    myChar.myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
                }
            }
        }

        // Phương thức CanUseSkill kiểm tra xem có thể sử dụng kỹ năng tấn công không
        private bool CanUseSkill(Char myChar)
        {
            var myskill = myChar.myskill;
            long currentTimeMillis = mSystem.currentTimeMillis();
            // Kiểm tra xem thời gian kể từ lần sử dụng kỹ năng gần nhất đã vượt quá thời gian hồi chiêu không
            return currentTimeMillis - myskill.lastTimeUseThisSkill > myskill.coolDown;
        }
    }
}
