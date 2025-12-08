import ClubList from "../components/ClubList";

const HomePage = () => {

  return (
    <div style={{ maxWidth: "800px", margin: "100px auto" }}>
      <h1>Kulüpler</h1>
      <ClubList />
    </div>
  );
};

export default HomePage;
