// <Copyright>Copyright (c) Birklid Software. All rights reserved.</Copyright>
// <File>TipsyTextBox.cs</File>
// <Company>Birklid Software</Company>
// <Author>Ray Birklid</Author>
// <Email>Ray.Birklid@gmail.com</Email>
// <Date>11/9/2021 8:26:17 AM</Date>
namespace Com.Gmail.Birklid.Ray.Tipsy.Controls
{
    using System.Windows.Controls;
    using System.Windows.Input;

    public class TipsyTextBox : TextBox
    {
        #region Creation

        public TipsyTextBox()
            : base()
        {
        }

        #endregion Creation

        #region Protected Members

        protected override void OnGotKeyboardFocus(
            KeyboardFocusChangedEventArgs e)
        {
            base.OnGotKeyboardFocus(e);
            SelectAll();
            e.Handled = true;
        }

        #endregion Protected Members
    }
}
