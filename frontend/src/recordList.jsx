function RecordList() {
  const records = [
    { id: 1, artist: "Seth Avett", album: "Sings Greg Brown", year: 2022, condition: "Mint", needReplacement: false },
    { id: 2, artist: "Phoebe Snow", album: "Phoebe Snow", year: 1974, condition: "Mint", needReplacement: false },
    { id: 3, artist: "Erykah Badu", album: "Baduizm", year: 1997, condition: "Mint", needReplacement: false }
  ];

  return (
    <div>
      <h2>My Records</h2>
      <ul>
        {records.map((record) => (
          <li key={record.id}>
            {record.artist} - {record.album} ({record.year}) - {record.condition} {record.needReplacement && "(Needs Replacement)"} 
          </li>
        ))}
      </ul>
    </div>
  );
}

export default RecordList;