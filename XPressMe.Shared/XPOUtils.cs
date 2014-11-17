using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Helpers;
using DevExpress.Xpo.Metadata;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace XPressMe.Shared
{
	public static class XPOUtils
	{
		public static Session GetNew(bool transactional)
		{
			return transactional ? GetNewUnitOfWork() : GetNewSession();
		}
		public static Session GetNewSession()
		{
			return new Session(DataLayer);
		}

		public static UnitOfWork GetNewUnitOfWork()
		{
			return new UnitOfWork(DataLayer);
		}

		public static void Commit(object obj)
		{
			if (obj == null)
				return;

			UnitOfWork wrk = obj as UnitOfWork;
			if (wrk != null)
				wrk.CommitChanges();
			else
			{
				ISessionProvider dataObject = obj as ISessionProvider;
				if (dataObject != null)
				{
					wrk = dataObject.Session as UnitOfWork;
					if (wrk != null)
						wrk.CommitChanges();
					dataObject.Session.Save(dataObject);
				}
			}
		}

		public static void Rollback(object obj)
		{
			if (obj == null)
				return;

			UnitOfWork wrk = obj as UnitOfWork;
			if (wrk != null)
				wrk.RollbackTransaction();
			else
			{
				ISessionProvider dataObject = obj as ISessionProvider;
				if (dataObject != null)
				{
					wrk = dataObject.Session as UnitOfWork;
					if (wrk != null)
						wrk.RollbackTransaction();
				}
			}
		}

		static IDataLayer _XpoDataLayer;
		public static IDataLayer DataLayer
		{
			get
			{
				if (_XpoDataLayer == null)
				{
					lock (typeof(XPOUtils))
					{
						_XpoDataLayer = GetDataLayer();
					}
				}
				return _XpoDataLayer;
			}
		}

		private static IDataLayer GetDataLayer()
		{
			// set XpoDefault.Session to null to prevent accidental use of XPO default session
			XpoDefault.Session = null;
			ReflectionClassInfo.SuppressSuspiciousMemberInheritanceCheck = true;
			// Needed to run in Medium Trust Security Context
			XpoDefault.UseFastAccessors = false;
			XpoDefault.IdentityMapBehavior = IdentityMapBehavior.Strong;

			string conn = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
			if (String.IsNullOrEmpty(conn))
				throw new Exception("No ConnectionString 'ApplicationServices' is specified in the web.config");

			XPDictionary dataDictionary = new ReflectionDictionary();
			IDataStore dataStore = XpoDefault.GetConnectionProvider(conn, AutoCreateOption.DatabaseAndSchema);
			// Initialize the XPO dictionary by going though all assemblies.
			dataDictionary.GetDataStoreSchema(AppDomain.CurrentDomain.GetAssemblies());
			// make sure everything exists in the db
			using (SimpleDataLayer dataLayer = new SimpleDataLayer(dataStore))
			{
				using (Session session = new Session(dataLayer))
				{
					// place code here to patch metadata
					session.UpdateSchema();
					session.CreateObjectTypeRecords();
					XpoDefault.DataLayer = new ThreadSafeDataLayer(session.Dictionary, dataStore);
					//XpoDefault.DataLayer = new ThreadSafeDataLayer(session.Dictionary, new DataCacheNode(new DataCacheRoot(dataStore)));
				}
			}

			IDataLayer result = new ThreadSafeDataLayer(dataDictionary, dataStore);


			return result;
		}
	}
}