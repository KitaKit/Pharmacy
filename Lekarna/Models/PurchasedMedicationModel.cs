namespace Pharmacy.Models
{
    public class PurchasedMedicationModel
    {
        public int MedicationId { get; set; }
        public int PurchasedId { get; set; }
        public int Count { get; set; }

        public PurchasedMedicationModel(int medicationId, int purchasedId, int count)
        {
            MedicationId = medicationId;
            PurchasedId = purchasedId;
            Count = count;
        }
    }
}
