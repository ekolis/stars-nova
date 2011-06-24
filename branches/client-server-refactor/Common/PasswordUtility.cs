#region Copyright Notice
// ============================================================================
// Copyright (C) 2011 The Stars-Nova Project
//
// This file is part of Stars-Nova.
// See <http://sourceforge.net/projects/stars-nova/>.
//
// This program is free software; you can redistribute it and/or modify
// it under the terms of the GNU General Public License version 2 as
// published by the Free Software Foundation.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>
// ===========================================================================
#endregion

using System;
using System.Security.Cryptography;
using System.Text;

namespace Nova.Common
{
    public class PasswordUtility
    {
        public string CalculateHash(string plainText)
        {
            byte[] plainBytes = ASCIIEncoding.Default.GetBytes(plainText);
            byte[] hashBytes = new MD5CryptoServiceProvider().ComputeHash(plainBytes);
            return BitConverter.ToString(hashBytes);
        }
    }
}
