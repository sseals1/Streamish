import React, { useEffect, useInsertionEffect, useState } from "react";
import Video from "./Video";
import { getAllVideos } from "../modules/videoManager";

export const VideoList = ({ searchTerm }) => {
  const [videos, setVideos] = useState([]);
  const [filteredVideos, setFilteredVideos] = useState([]);

  const getVideos = () => {
    getAllVideos().then((videos) => setVideos(videos));
    getAllVideos().then((videos) => setFilteredVideos(videos));
  };

  useEffect(() => {
    getVideos();
  }, []);

  useEffect(() => {
    const searchedVideos = videos.filter((video) => {
      return video.title.toLowerCase().startsWith(searchTerm.toLowerCase());
    });
    setFilteredVideos(searchedVideos);
  }, [searchTerm]);

  return (
    <div className="container">
      <div className="row justify-content-center">
        {filteredVideos.map((video) => (
          <Video video={video} key={video.id} />
        ))}
      </div>
    </div>
  );
};
