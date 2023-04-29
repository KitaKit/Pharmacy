using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharmacy.Sorting_models
{
    public static class SearchModel
    {
        public static dynamic GetSearchedRows(string searchedWord, DataLists dataLists)
        {
            if (string.IsNullOrEmpty(searchedWord))
            {
                DataGridTables.ShowDataToTable(dataLists);
                return null;
            }
            else if (string.IsNullOrWhiteSpace(searchedWord))
                return null;
            else
            {
                var selectedTable = DataGridTables.GetSelectedTable();
                switch (selectedTable)
                {
                    case SelectedTable.Medications:
                        return dataLists.MedicationsData.Where(x => x.Id.ToString().ToLower().Contains(searchedWord) || x.Title.ToLower().Contains(searchedWord) || x.Description.ToLower().Contains(searchedWord)).Distinct().ToList();
                    case SelectedTable.Warehouses:
                        return dataLists.WarehousesData.Where(x => x.Id.ToString().ToLower().Contains(searchedWord) || x.Name.ToLower().Contains(searchedWord) || x.Medications.ToLower().Contains(searchedWord)).Distinct().ToList();
                    case SelectedTable.Manufacturers:
                        return dataLists.ManufacturersData.Where(x => x.Id.ToString().ToLower().Contains(searchedWord) || x.Name.ToLower().Contains(searchedWord) || x.Country.ToLower().Contains(searchedWord) || x.License.Contains(searchedWord) || x.Medications.ToLower().Contains(searchedWord)).Distinct().ToList();
                    case SelectedTable.Sales:
                        return dataLists.SalesData.Where(x => x.Id.ToString().ToLower().Contains(searchedWord) || x.Medications.ToLower().Contains(searchedWord)).Distinct().ToList();
                    case SelectedTable.Purchases:
                        return dataLists.PurchasesData.Where(x => x.Id.ToString().ToLower().Contains(searchedWord) || x.Medications.ToLower().Contains(searchedWord)).Distinct().ToList();
                }
            }
            return null;
        }
    }
}
