using Microsoft.Playwright;

namespace pre.test.pages
{
    public class BasePage
    {
        protected IPage Page;
        public BasePage(IPage page) => Page = page;
        public IPage GetPage() => Page;
    }
}
