﻿/*This file is part of AutoText.

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
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoText.Forms
{
	public partial class FormAbout : Form
	{
		public FormAbout()
		{
			InitializeComponent();
			labelVersion.Text = string.Format(labelVersion.Text, Constants.CommonConstants.ApplicationVersion);
			labelAuthor.Text = string.Format(Constants.CommonConstants.ApplicationAuthorCopy, DateTime.Now.Year);
		}

		private void FormAbout_Load(object sender, EventArgs e)
		{

		}

		private void FormAbout_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Escape)
			{
				Close();
			}
		}
	}
}
