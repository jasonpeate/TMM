namespace TMM.Logic
{
    public sealed class ReponseResult
    {
        public bool Result { get; }

        public string Message { get; }

        public int? ID { get; }

        public static ReponseResult Success(int? ID = null)
        {
            return new ReponseResult(true, "Success", ID);
        }

        public static ReponseResult ValidaitionFailed(params string[] Messages)
        {
            return new ReponseResult(false, string.Join("|",Messages));
        }

        public static ReponseResult Exception(Exception ex)
        {
            return new ReponseResult(false, ex.Message);
        }

        private ReponseResult(bool Result, string Message, int? ID) : this(Result,Message)
        {
            this.ID = ID;
        }

        private ReponseResult(bool Result, string Message)
        {
            this.Result = Result;
            this.Message = Message;
        }
    }
}
