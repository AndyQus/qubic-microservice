print("Seeding data into QubicMicroserviceDB...");

db = db.getSiblingDB("QubicMicroserviceDB");

db.createCollection("transactions");
db.transactions.insertMany([
  {
    transactionType: "send",
    amount: 1500.0,
    date: ISODate("2024-01-01T00:00:00Z"),
    description: "qubic send transaction",
  },
  {
    transactionType: "receive",
    amount: 500.0,
    date: ISODate("2024-01-02T00:00:00Z"),
    description: "qubic receive transaction",
  },
]);

print("Data seeding completed.");
