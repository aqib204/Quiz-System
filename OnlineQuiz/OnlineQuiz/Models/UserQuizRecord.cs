//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel;

namespace OnlineQuiz.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserQuizRecord
    {
        public int QuizID { get; set; }
        public int UserID { get; set; }
        public string Username { get; set; }

        [DisplayName("Subject Name")]
        public string SubjectName { get; set; }

        [DisplayName("Quiz Date")]
        public Nullable<System.DateTime> QuizDate { get; set; }

        [DisplayName("Marks Obtained")]
        public Nullable<int> MarksObtained { get; set; }
    
        public virtual Login Login { get; set; }
    }
}
