using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrelloNet;

namespace ReadTextFile.Services.TrelloServices
{
    public class Authentication
    {
        public void Test()
        {
            Trello trello = new Trello("e679fb1003750a1a10e50088db874c9d");

            //var urlForAuthentication = trello.GetAuthorizationUrl("[a name for your application]", Scope.ReadOnly);

            Token token = new Token();

            

            // The user will receive a token, call Authenticate with it
            trello.Authorize("");

            IEnumerable<Board> enumerable =  trello.Boards.ForMe(BoardFilter.Organization);

            // Quadro template.
            Board templateBoard = trello.Boards.WithId("YeNKwJVK");


            Board newBoardTest = trello.Boards.Add(new NewBoard("Teste"));            

            IEnumerable<List> templateBoardLists = trello.Lists.ForBoard(templateBoard);
            IEnumerable<List> newBoardLists = trello.Lists.ForBoard(newBoardTest);

            List<List> l = templateBoardLists.ToList();

            var r = newBoardLists.ElementAt(0);

            int i = 0;

            foreach (List item in newBoardLists)
            {
                item.Name = l[i].Name;
                trello.Lists.Update(item);

                l.Remove(l[i]);
            }

            foreach (List item in l)
            {
                trello.Lists.Add(item.Name, newBoardTest);
            }
            

            

            int x = 0;

        }

    }
}
