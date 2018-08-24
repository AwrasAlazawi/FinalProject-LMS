namespace FinalProject_LMS.Models
{
    public class UMessage
    {
        public int Id { get; set; }

        public string SenderId { get; set; }

        public string Rubrik { get; set; }

        public string Text { get; set; }

        public ApplicationUser Reciver { get; set; }
        public string ReciverId { get; set; }

        public int IsReaden { get; set; }
    }
}