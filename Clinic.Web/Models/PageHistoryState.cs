namespace Clinic.Web.Models
{
    public class PageHistoryState
    {
        private List<string> previousPages;

        public PageHistoryState()
        {
            previousPages = new List<string>();
        }
        public void AddPage(string pageName)
        {
            if (previousPages.Count > 0 && previousPages[previousPages.Count - 1] == pageName)
                return;

            // Go upto 20 pages
            if (previousPages.Count > 20)
                previousPages.Clear();
            previousPages.Add(pageName);
        }

        public string PreviousPage()
        {
            if (previousPages.Count > 1)
            {
                // You add a page on initialization, so you need to return the 2nd from the last
                return previousPages.ElementAt(previousPages.Count - 2);
            }

            // Can't go back because you didn't navigate enough
            return previousPages.FirstOrDefault();
        }

        public bool CanGoBack()
        {
            return previousPages.Count > 1;
        }
    }
}
