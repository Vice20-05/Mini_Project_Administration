using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mini_Project_Administration
{
    // Lab 5 requirement (async task): every database operation runs on the Thread Pool (Task.Run),
    // so the window does not freeze. When it is done, Invoke() sends the result back to the GUI thread.
    // All forms (Form1, CategoryForm, SalesHistoryForm) use this helper.
    internal static class DbTask
    {
        // window = the form that waits for the result
        // dbWork = what to do with the database (runs on a worker thread, must not touch controls)
        // done   = called on the GUI thread with the result, or with the error message (result is default then)
        public static void Run<T>(Form window, Func<ShopDbContext, T> dbWork, Action<T, string> done)
        {
            Task.Run(() =>
            {
                T result = default(T);
                string error = null;
                try
                {
                    using (ShopDbContext db = new ShopDbContext())
                    {
                        result = dbWork(db);
                    }
                }
                catch (Exception ex)
                {
                    // show the innermost exception, that one has the real reason (e.g. SQLite error)
                    while (ex.InnerException != null)
                        ex = ex.InnerException;
                    error = ex.Message;
                }

                // back to the GUI thread (only if the window is still open)
                if (!window.IsDisposed)
                    window.BeginInvoke((Action)(() => done(result, error)));
            });
        }
    }
}
