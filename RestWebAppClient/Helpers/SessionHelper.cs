using RestWebAppClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace RestWebAppClient.Helpers
{
    /// <summary>
    /// Slouzi k ukladani tokenu kolekce emlpoyee do session
    /// </summary>
    public class SessionHelper
    {
        private readonly HttpSessionStateBase m_Session;
        public SessionHelper(HttpSessionStateBase session)
        {
            m_Session = session;
        }

        public AuthenticationToken Token
        {
            get
            {
                return m_Session["Token"] as AuthenticationToken;
            }
            set
            {
                m_Session["Token"] = value;
            }
        }
    }
}
