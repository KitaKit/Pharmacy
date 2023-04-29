namespace Pharmacy.Models
{
    public class SoldMedicationModel
    {
        public int SaleId { get; set; }
        public int MedicationId { get; set; }
        public int Count { get; set; }

        public SoldMedicationModel(int saleId, int medicationId, int count)
        {
            SaleId = saleId;
            MedicationId = medicationId;
            Count = count;
        }
    }
}
