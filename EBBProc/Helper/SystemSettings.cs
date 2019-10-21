using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EBBProc.Helper
{
    public class SystemSettings
    {
        public static bool GetBooleanValue(string Key)
        {
            using (var db = new EBBPEntities())
            {
                GlobalSetting setting = db.GlobalSettings.Where(x => x.Name.ToUpper() == Key.ToUpper()).FirstOrDefault();

                if (null == setting)
                    throw new Exception("Setting not found!");

                if ("Bool" != setting.Type)
                    throw new Exception("Data type mismatch!");

                bool value = new bool();
                if (!bool.TryParse(setting.Value, out value))
                {
                    throw new Exception("Value is not of data type bool!");
                }

                return value;
            }
        }

        public static DateTime GetDateTimeValue(string Key)
        {
            using (var db = new EBBPEntities())
            {
                GlobalSetting setting = db.GlobalSettings.Where(x => x.Name.ToUpper() == Key.ToUpper()).FirstOrDefault();

                if (null == setting)
                    throw new Exception("Setting not found!");

                if ("DateTime" != setting.Type)
                    throw new Exception("Data type mismatch!");

                DateTime value = new DateTime();
                if (!DateTime.TryParse(setting.Value, out value))
                {
                    throw new Exception("Value is not of data type DateTime!");
                }

                return value;
            }
        }

        public static Decimal GetDecimalValue(string Key)
        {
            using (var db = new EBBPEntities())
            {
                GlobalSetting setting = db.GlobalSettings.Where(x => x.Name.ToUpper() == Key.ToUpper()).FirstOrDefault();

                if (null == setting)
                    throw new Exception("Setting not found!");

                if ("Decimal" != setting.Type)
                    throw new Exception("Data type mismatch!");

                Decimal value = new Decimal();
                if (!Decimal.TryParse(setting.Value, out value))
                {
                    throw new Exception("Value is not of data type Decimal!");
                }

                return value;
            }
        }

        public static Double GetDoubleValue(string Key)
        {
            using (var db = new EBBPEntities())
            {
                GlobalSetting setting = db.GlobalSettings.Where(x => x.Name.ToUpper() == Key.ToUpper()).FirstOrDefault();

                if (null == setting)
                    throw new Exception("Setting not found!");

                if ("Double" != setting.Type)
                    throw new Exception("Data type mismatch!");

                Double value = new Double();
                if (!Double.TryParse(setting.Value, out value))
                {
                    throw new Exception("Value is not of data type Double!");
                }

                return value;
            }
        }

        public static float GetFloatValue(string Key)
        {
            using (var db = new EBBPEntities())
            {
                GlobalSetting setting = db.GlobalSettings.Where(x => x.Name.ToUpper() == Key.ToUpper()).FirstOrDefault();

                if (null == setting)
                    throw new Exception("Setting not found!");

                if ("Float" != setting.Type)
                    throw new Exception("Data type mismatch!");

                float value = new float();
                if (!float.TryParse(setting.Value, out value))
                {
                    throw new Exception("Value is not of data type float!");
                }

                return value;
            }
        }

        public static int GetIntValue(string Key)
        {
            using (var db = new EBBPEntities())
            {
                GlobalSetting setting = db.GlobalSettings.Where(x => x.Name.ToUpper() == Key.ToUpper()).FirstOrDefault();

                if (null == setting)
                    throw new Exception("Setting not found!");

                if ("Int" != setting.Type)
                    throw new Exception("Data type mismatch");

                int value = new int();
                if (!Int32.TryParse(setting.Value, out value))
                {
                    throw new Exception("Value is not of data type int");
                }

                return value;
            }
        }

        public static long GetLongValue(string Key)
        {
            using (var db = new EBBPEntities())
            {
                GlobalSetting setting = db.GlobalSettings.Where(x => x.Name.ToUpper() == Key.ToUpper()).FirstOrDefault();

                if (null == setting)
                    throw new Exception("Setting not found!");

                if ("Long" != setting.Type)
                    throw new Exception("Data type mismatch!");

                long value = new long();
                if (!long.TryParse(setting.Value, out value))
                {
                    throw new Exception("Value is not of data type Long!");
                }

                return value;
            }
        }

        public static String GetStringValue(string Key)
        {
            using (var db = new EBBPEntities())
            {
                GlobalSetting setting = db.GlobalSettings.Where(x => x.Name.ToUpper() == Key.ToUpper()).FirstOrDefault();

                if (null == setting)
                    throw new Exception("Setting not found!");

                if ("String" != setting.Type)
                    throw new Exception("Data type mismatch!");

                return setting.Value;
            }
        }
    }
}