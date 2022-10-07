import React from "react";
import "./App.css";
import { SearchList } from "./components/SearchList";
import { VideoContainer } from "./components/VideoContainer";
import { VideoList } from "./components/VideoList";

function App() {
  return (
    <div className="App">
      <VideoContainer />
    </div>
  );
}

export default App;
