import { useEffect, useState } from "react";
import Login from "./Login";
import { Spin } from "antd";

const initialAuthState = {
  userId: 0,
  userFullName: "",
};
function Authenticator(props) {
  const [checking, setChecking] = useState(true);
  const [authInfo, setAuthInfo] = useState(initialAuthState);

  const didMount = async () => {
    let response = await fetch("api/checkLoggedIn", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({}),
    });
    if (response.status === 401) {
      setChecking(false);
      return;
    }

    let respJson = await response.json();

    if (respJson.statusCode === 200) {
      setAuthInfo({
        userId: respJson.data.userId,
        userFullName: respJson.data.userFullName,
      });
    }
    setChecking(false);
  };

  useEffect(() => {
    didMount();
  }, []);

  const handleLoginSuccess = (userInfo) => {
    console.log("handleLoginSuccess", userInfo);
    setAuthInfo({
      userId: userInfo.userId,
      userFullName: userInfo.userFullName,
    });
  };

  if (checking) {
    return <Spin />;
  }
  if (authInfo.userId === 0) {
    return <Login onLoginSuccess={handleLoginSuccess} />;
  }
  return <> {props.children(authInfo)}</>;
}

export default Authenticator;
