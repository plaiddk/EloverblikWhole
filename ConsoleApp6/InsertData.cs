using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConsoleApp6
{
    class InsertData
    {

        public static void InsertDataSQL(DataSet _dataset, string option)
        {

            using (SqlConnection conn = new SqlConnection(@"Data Source=.\SQLEXPRESS; Initial Catalog=FuckingNice; Integrated Security=True"))
            {
                using (SqlBulkCopy bulk = new SqlBulkCopy(conn))
                {

                    foreach (DataTable item in _dataset.Tables)
                    {

                        if (option.Equals("insertmetering"))
                        {

                            if (!item.TableName.Equals("sender_MarketParticipant.mRID") && !item.TableName.Equals("period.timeInterval"))
                            {
                                bulk.DestinationTableName = item.TableName;
                                conn.Open();

                                bulk.WriteToServer(item);
                                conn.Close();

                            }

                            if (item.TableName.Equals("period.timeInterval"))
                            {
                                bulk.DestinationTableName = "periodtimeInterval";
                                conn.Open();

                                bulk.WriteToServer(item);
                                conn.Close();

                            }

                            else
                            {
                                //donothing
                            }
                        }

                        if (option.Equals("insertprices"))
                        {
                            if (!item.TableName.Equals("result"))
                            {
                                bulk.DestinationTableName = item.TableName;
                                conn.Open();

                                bulk.WriteToServer(item);
                                conn.Close();

                            }
                            else
                            {
                                bulk.DestinationTableName = "resultPrice";
                                conn.Open();

                                bulk.WriteToServer(item);
                                conn.Close();

                            }
                        }
                    }
                }

            }
        }
    }
}
