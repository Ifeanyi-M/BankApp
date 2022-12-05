using System.Runtime.ConstrainedExecution;

namespace MYBANKAPP.UI
{
    public class APPMIRRORHelpers
    {
        public string WelcomeCustomer(string fullname)
        {
            return $"Welcome back, {fullname}";
            Service.HitEnterKey();


        }

        public int PickAmount()
        {
            Console.WriteLine(" ");
            int cur = 0;
            Console.WriteLine(":1. {0}500        5.{0}10000", cur);
            Console.WriteLine(":2. {0}1000       6.{0}15000", cur);
            Console.WriteLine(":3. {0}2000       7.{0}20000", cur);
            Console.WriteLine(":4. {0}5000       8.{0}50000", cur);
            Console.WriteLine(":0. {0}Other", cur);
            Console.WriteLine("");

            int chosenAmount = Validator.Transform<int>("option");
            switch (chosenAmount)
            {
                case 1:
                    return 500;
                    break;
                case 2:
                    return 1000;
                    break;
                case 3:
                    return 2000;
                    break;
                case 4:
                    return 5000;
                    break;
                case 5:
                    return 10000;
                    break;
                case 6:
                    return 15000;
                    break;
                case 7:
                    return 20000;
                    break;
                case 8:
                    return 50000;
                case 0:
                    return 0;
                    break;
                default:
                    Service.PrintMessage("Wrong Input. Please retry!", false);

                    return -1;
                    break;


            }
        }



    }
}