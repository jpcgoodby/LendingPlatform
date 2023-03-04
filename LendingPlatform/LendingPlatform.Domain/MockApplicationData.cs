using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LendingPlatform.Domain
{
    public class MockApplicationData
    {
        public static List<LoanApplication> LoadData()
        {
            var root = AppDomain.CurrentDomain.BaseDirectory;
            var solutionRoot = root.Replace("\\LendingPlatform\\LendingPlatform.Service\\bin\\Debug\\net7.0", "");
            var filePath = solutionRoot + "Data\\data.json";

            return JsonConvert.DeserializeObject<List<LoanApplication>>(File.ReadAllText(filePath));
            
        }
    }
}
