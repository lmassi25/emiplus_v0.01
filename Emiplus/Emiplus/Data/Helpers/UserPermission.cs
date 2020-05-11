using System.Windows.Forms;
using VisualPlus.Toolkit.Controls.Interactivity;

namespace Emiplus.Data.Helpers
{
    internal class UserPermission
    {
        public static bool SetControl(Button button, PictureBox img, string tela, bool message = true)
        {
            if (GetPermission(tela))
                return false;

            img.Visible = true;
            button.Cursor = Cursors.No;

            if (message)
                Alert.Message("Oppss", "Você não possui permissão para acessar essa área.", Alert.AlertType.warning);

            return true;

        }

        public static bool SetControlVisual(VisualButton button, PictureBox img, string tela, bool message = true)
        {
            if (GetPermission(tela))
                return false;

            img.Visible = true;
            button.Cursor = Cursors.No;

            if (message)
                Alert.Message("Oppss", "Você não possui permissão para acessar essa área.", Alert.AlertType.warning);

            return true;
        }

        public static bool SetControlLabel(Label button, PictureBox img, string tela)
        {
            if (GetPermission(tela))
                return false;

            img.Visible = true;
            button.Cursor = Cursors.No;
            Alert.Message("Oppss", "Você não possui permissão para acessar essa área.", Alert.AlertType.warning);

            return true;
        }

        private static bool GetPermission(string tela)
        {
            if (Program.UserPermissions.Count == 1 && Program.UserPermissions[0].ToString() == "{ all = 1 }")
                return true;

            foreach (dynamic item in Program.UserPermissions)
                if (item.Key == tela)
                    return item.Value == "1";

            return true;
        }
    }
}