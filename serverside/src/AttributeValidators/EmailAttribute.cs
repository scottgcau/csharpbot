/*
 * @bot-written
 * 
 * WARNING AND NOTICE
 * Any access, download, storage, and/or use of this source code is subject to the terms and conditions of the
 * Full Software Licence as accepted by you before being granted access to this source code and other materials,
 * the terms of which can be accessed on the Codebots website at https://codebots.com/full-software-license. Any
 * commercial use in contravention of the terms of the Full Software Licence may be pursued by Codebots through
 * licence termination and further legal action, and be required to indemnify Codebots for any loss or damage,
 * including interest and costs. You are deemed to have accepted the terms of the Full Software Licence on any
 * access, download, storage, and/or use of this source code.
 * 
 * BOT WARNING
 * This file is bot-written.
 * Any changes out side of "protected regions" will be lost next time the bot makes any changes.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Sportstats.Validators
{
    public class EmailAttribute: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var dispayName = validationContext.DisplayName;
			// convert object to string
            string stringValue = value != null? value.ToString(): "";
			// do not validate any format when the value is empty, 'required' validator will deal with it
            if (string.IsNullOrEmpty(stringValue))
            {
                return ValidationResult.Success;
            }
            else
            {

                try
                {
					// By using this constructor, it's actually using the .net official logic to test if it's an email
                    MailAddress m = new MailAddress(stringValue);

                    return ValidationResult.Success;
                }
                catch (FormatException)
                {
                    return new ValidationResult($"{dispayName} is not a valid email");
                }
            }
        }
    }
}
