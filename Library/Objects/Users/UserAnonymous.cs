using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSI.Library.Objects.Users
{
    internal class UserAnonymous : User
    {
        internal UserAnonymous(Security.Credential credential)
            : base(0, DateTime.Now, "", "", "", 0, new Handlers.Languages().ItemDefault().IdLanguage, true, credential)
        { }

    }
}
