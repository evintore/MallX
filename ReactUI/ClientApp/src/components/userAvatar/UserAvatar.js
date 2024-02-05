import React from "react";
import { Avatar, Menu, Dropdown } from "antd";
import { UserOutlined, PoweroffOutlined } from "@ant-design/icons";

export const UserAvatar = () => {
  const logoutHandler = async () => {
    let response = await fetch("api/logout", {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
      },
    });

    if (response.status === 200) window.location.href = "/";
  };

  const widgetMenu = (
    <Menu>
      <Menu.Item onClick={logoutHandler}>
        <PoweroffOutlined
          style={{
            display: "inline-flex",
            justifyContent: "center",
            alignItems: "center",
            marginRight: "5px",
          }}
        />
        Çıkış Yap
      </Menu.Item>
    </Menu>
  );

  return (
    <Dropdown overlay={widgetMenu}>
      <Avatar
        icon={<UserOutlined />}
        style={{
          display: "inline-flex",
          justifyContent: "center",
          alignItems: "center",
          marginLeft: "10px",
        }}
      />
    </Dropdown>
  );
};
