using LinqPractice;

namespace LinqPractices
{
    class DataGenerater
    {
        public static void Initialize(){
            using (var context=new LinqDbContext()){
                if (context.Students.Any())
                {
                    return;
                }
                else
                {
                    context.Students.AddRange(
                        new Student(){
                            Id=1,
                            FirstName="Atilla",
                            LastName="Kalay",
                            ClassId=4
                        },
                         new Student(){
                            Id=2,
                            FirstName="Rabia",
                            LastName="Tanış",
                            ClassId=4
                        }, new Student(){
                            Id=3,
                            FirstName="Ufuk",
                            LastName="Beşir",
                            ClassId=4
                        }, new Student(){
                            Id=4,
                            FirstName="Adem",
                            LastName="Yıldırım",
                            ClassId=4
                        }
                    );
                }
            }
        }

    }
}