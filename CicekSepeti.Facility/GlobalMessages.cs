using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CicekSepeti.Facility
{
    public enum GlobalMessages
    {
        [Description("This Email address is in use. Please select another email address to continue registration.")]
        EmailIsInUse = 1,

        [Description("An error occured. Please try again.")]
        ErrorWhileTransaction = 2,

        [Description("Incorrect Email Address. Please Try again.")]
        EmailIsNotExist = 3,

        [Description("Incorrect Password. Please Try again.")]
        PasswordIsNotCorrect = 4,
    }
}
