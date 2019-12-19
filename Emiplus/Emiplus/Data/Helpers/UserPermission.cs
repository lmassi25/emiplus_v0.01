using System.Windows.Forms;

namespace Emiplus.Data.Helpers
{
    class UserPermission
    {   
        public static bool SetControl(Button button, PictureBox img, string tela)
        {
            if (!GetPermission(tela))
            {
                img.Visible = true;

                button.Cursor = Cursors.No;
                Alert.Message("Oppss", "Você não possui permissão para acessar essa área.", Alert.AlertType.warning);

                return true;
            }

            return false;
        }

        private static bool GetPermission(string tela)
        {
            if (Program.userPermissions.Count == 1 && Program.userPermissions[0].ToString() == "{ all = 1 }")
                return true;

            foreach (dynamic item in Program.userPermissions)
            {
                if (item.Key == tela)
                {
                    if (item.Value == "1")
                        return true;

                    return false;
                }
            }
            
            return true;
        }
    }
}
