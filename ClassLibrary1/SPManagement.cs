using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SharePoint.Taxonomy;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

namespace SPManagement
{
    public class SPManagement
    {
        /// <summary>
        /// Gets the current version document count for the given libraryName and siteURL
        /// </summary>
        /// <param name="siteURL">Provide absolute url for the site</param>
        /// <param name="libraryName">Provide the library's display name</param>
        /// <returns></returns>
        public static int getDocCount(string siteURL, string libraryName)
        {
            int count = 0;

            try
            {
                using (SPSite site = new SPSite(siteURL))
                {
                    using (SPWeb web = site.OpenWeb())
                    {
                        SPList list = web.Lists[libraryName];
                        SPQuery spQuery = new SPQuery();
                        spQuery.ViewAttributes = "Scope=\"Recursive\"";
                        spQuery.RowLimit = 5000;

                        do
                        {
                            SPListItemCollection listItems = list.GetItems(spQuery);
                            spQuery.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
                            count += listItems.Count;
                        }
                        while (spQuery.ListItemCollectionPosition != null);
                    }
                }
            }
            catch(Exception e)
            {
                throw e;
            }
            return count;
        }

        /// <summary>
        /// Gets the All version count for the given libraryName and siteURL
        /// </summary>
        /// <param name="siteURL">Provide absolute url for the site</param>
        /// <param name="libraryName">Provide the library's display name</param>
        /// <returns></returns>
        public static int getVersionCount(string siteURL, string libraryName)
        {
            int vcount = 0;
            using (SPSite site = new SPSite(siteURL))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[libraryName];
                    SPQuery spQuery = new SPQuery();
                    spQuery.ViewAttributes = "Scope=\"Recursive\"";
                    spQuery.RowLimit = 5000;

                    do
                    {
                        SPListItemCollection listItems = list.GetItems(spQuery);

                        foreach (SPListItem item in listItems)
                        {
                            vcount += item.Versions.Count;
                        }
                        spQuery.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
                    }
                    while (spQuery.ListItemCollectionPosition != null);
                }
            }
            return vcount;
        }

        /// <summary>
        /// Gets the relative document URL of the first document with the largest size in the library, size in MB, total current version size of the library in MB. Separated by "|" symbol
        /// </summary>
        /// <param name="siteURL">Provide absolute url for the site</param>
        /// <param name="libraryName">Provide the library's display name</param>
        /// <returns></returns>
        public static string getMaxSizeDocument(string siteURL, string libraryName)
        {
            int maxSize = 0;
            double totalSize = 0;
            int size = 0;
            string docURL = string.Empty;
            using (SPSite site = new SPSite(siteURL))
            {
                using (SPWeb web = site.OpenWeb())
                {
                    SPList list = web.Lists[libraryName];
                    SPQuery spQuery = new SPQuery();
                    spQuery.ViewAttributes = "Scope=\"Recursive\"";
                    spQuery.RowLimit = 5000;

                    do
                    {
                        SPListItemCollection listItems = list.GetItems(spQuery);

                        foreach (SPListItem item in listItems)
                        {
                            size = Convert.ToInt32(item["File Size"]);
                            totalSize += (double)size / 1024.0 / 1024.0;
                            if (maxSize < size)
                            {
                                maxSize = size;
                                docURL = item.Url;
                            }
                        }
                        spQuery.ListItemCollectionPosition = listItems.ListItemCollectionPosition;
                    }
                    while (spQuery.ListItemCollectionPosition != null);
                }
            }
            return docURL +"|"+ maxSize + " Bytes|"+totalSize+" MB";
        }

        #region Term Store
        /// <summary>
        /// Gets the All Term store collection for given site
        /// </summary>
        /// <param name="oSite">SPsite object</param>
        /// <returns>TermStoreCollection</returns>
        public TermStoreCollection getTermStoreCollection(SPSite oSite)
        {
            TermStoreCollection oTermStores = null;
            TaxonomySession oSession = new TaxonomySession(oSite);
            if (oSession != null)
            {
                oTermStores = oSession.TermStores;
            }
            return oTermStores;
        }
        #endregion 
    }
}
