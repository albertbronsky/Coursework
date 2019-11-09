namespace Coursework.Authorization
{
    public class Constants
    {
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string ApproveOperationName = "Approve";
        public static readonly string RejectOperationName = "Reject";

        public static readonly string QuestionAdministratorsRole =
            "QuestionAdministrators";
        public static readonly string QuestionManagersRole = "QuestionManagers";
        public static readonly string AnswerAdministratorsRole =
            "AnswerAdministrators";
        public static readonly string AnswerManagersRole = "AnswerManagers";
    }
}