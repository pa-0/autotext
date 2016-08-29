/*This file is part of AutoText.

Copyright © 2016 Alexander Litvinov

AutoText is free software; you can redistribute it and/or
modify it under the terms of the GNU General Public License
as published by the Free Software Foundation; either version 2
of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */


using System;
using System.Windows.Forms;

namespace AutoText.Forms
{
    public partial class ErrorMessageForm : Form
    {
        public ErrorMessageForm(string customMessage, Exception exceptionOccured, bool isErrorCrytical)
        {
            InitializeComponent();
            labelErrorMessage.Text = exceptionOccured.Message;
            textBoxExceptionDetails.Text = exceptionOccured.ToString();
            labelErrorCrytical.Visible = isErrorCrytical;

            if (isErrorCrytical)
            {
                buttonAction.Text = "Exit";
            }
            else
            {
                buttonAction.Text = "Ok";
            }
        }

        private void buttonAction_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
