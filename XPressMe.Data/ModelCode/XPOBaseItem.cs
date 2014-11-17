using System;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;
using System.Threading;
namespace XPressMe.Data.XPressMeDemoDB
{

    public partial class XPOBaseItem
    {
        public XPOBaseItem(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        protected override void OnSaving()
        {
            bool hasRequest = (HttpContext.Current != null && HttpContext.Current.Request != null);
            bool isAuthenticated = (Thread.CurrentPrincipal != null) && (Thread.CurrentPrincipal.Identity != null) && (Thread.CurrentPrincipal.Identity.IsAuthenticated);

            DateTime stamp = DateTime.UtcNow;
            string currentIP = hasRequest ? HttpContext.Current.Request.UserHostAddress : "127.0.0.1";
            string currentUser = String.Empty;
            if (!isAuthenticated && hasRequest)
                currentUser = HttpContext.Current.Request.AnonymousID;
            else
                currentUser = isAuthenticated ? Thread.CurrentPrincipal.Identity.Name : "Anonymous";

            if (Session.IsNewObject(this))
            {
                // inserting so update Add Fields				
                fAddUser = currentUser;
                fAddIP = currentIP;
                fAddStamp = stamp;
            }
            fModUser = currentUser;
            fModIP = currentIP;
            fModStamp = stamp;
            base.OnSaving();
        }

        public abstract new class Fields
        {
            protected Fields() { }

            public static OperandProperty ID { get { return new OperandProperty("ID"); } }
            public static OperandProperty AddStamp { get { return new OperandProperty("AddStamp"); } }
            public static OperandProperty AddUser { get { return new OperandProperty("AddUser"); } }
            public static OperandProperty AddIP { get { return new OperandProperty("AddIP"); } }
            public static OperandProperty ModStamp { get { return new OperandProperty("ModStamp"); } }
            public static OperandProperty ModUser { get { return new OperandProperty("ModUser"); } }
            public static OperandProperty ModIP { get { return new OperandProperty("ModIP"); } }
        }
    }

}
