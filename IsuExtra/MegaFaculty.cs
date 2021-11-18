namespace IsuExtra
{
    public enum MegaFacultyName
    {
        KTU,
        TINT,
        FTMI,
    }

    public class MegaFaculty
    {
        public MegaFaculty(MegaFacultyName name)
        {
            Name = name;
        }

        public MegaFacultyName Name
        {
            get;
        }
    }
}