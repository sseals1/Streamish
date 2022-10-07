const baseUrl = "/api/video";

export const getAllVideos = () => {
  return fetch(baseUrl + "/GetAllWithComments").then((res) => res.json());
};

export const getAllSearchTerms = () => {
  return fetch(baseUrl + "/searchVideos").then((res) => res.json());
};

export const addVideo = (video) => {
  return fetch(baseUrl, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(video),
  });
};
