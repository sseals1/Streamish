import { useState } from "react";
import { SearchList } from "./SearchList";
import { VideoList } from "./VideoList";

export const VideoContainer = () => {
  const [searchTerm, setSearchTerm] = useState("");
  return (
    <>
      <SearchList setterFunction={setSearchTerm} />
      <VideoList searchTerm={searchTerm} />
    </>
  );
};
