using System.Linq;

namespace EasyConsole.Data
{
    public class Answer
    {
        public string[] Answers { get; set; }
        
        /// <summary>
        /// Construct new answer
        /// </summary>
        /// <param name="answers">Possible inputs</param>
        public Answer(params string[] answers)
        {
            Answers = answers;
        }

        /// <summary>
        /// Check if answer meets input
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public bool IsMet(string input) =>
            Answers.Any(a => 
                a.ToLower().Trim(' ').Equals(input.ToLower().Trim(' ')) ||
                char.ToLower(a[0]).Equals(char.ToLower(input[0])));
        
    }
}