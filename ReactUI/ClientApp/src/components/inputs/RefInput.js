import React, { useEffect, useState } from "react";

export const RefInput = (props) => {
  const { query, property } = props;
  const [data, setData] = useState();

  useEffect(() => {
    if (!query || !property) return null;

    fetch(query)
      .then((response) => response.json())
      .then((data) => {
        setData(data.data);
      })
      .catch((err) => {
        console.log({ err });
        return null;
      });
  }, []);

  return <>{data && <span>{data[property]}</span>}</>;
};

export default RefInput;
