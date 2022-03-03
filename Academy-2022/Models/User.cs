namespace Academy_2022.Models
{
    public class User
    {
        private int _id;

        public int Id 
        { 
            get
            {
                return _id;
            }

            set 
            {
                _id = value;
            }
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName 
        {
            get 
            {
                return $"{FirstName} {LastName}";
            }        
        }

        // prop TAB TAB
        public int Age { get; set; }
    }
}
