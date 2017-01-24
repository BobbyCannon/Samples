
// Create some sample data


var start = Addresses.Count() + 1;

for (var i = start; i < 1000 + start; i++)
{
	Addresses.InsertOnSubmit(new Addresses {
		City = "City " + i,
		CreatedOn = DateTime.Now,
		Line1 = "Line " + i,
		Line2 = string.Empty,
		ModifiedOn = DateTime.Now,
		Postal  = "Postal " + i,
		State = i.ToString(),
		SyncId = Guid.NewGuid()
	});
}

SubmitChanges();