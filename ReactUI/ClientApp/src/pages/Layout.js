import React, { useState, useContext } from "react";
import { Layout, Menu, Card } from "antd";
import styles from "./Layout.module.css";

import { MenuUnfoldOutlined, MenuFoldOutlined } from "@ant-design/icons";
import { Link } from "react-router-dom";
import { AuthContext } from "../App";
import { UserAvatar } from "../components/userAvatar/UserAvatar";
const { Header, Sider, Content } = Layout;

function MainLayout(props) {
  const [collapsed, setCollapsed] = useState(false);
  const authInfo = useContext(AuthContext);
  console.log("authInfo", authInfo);
  function toggleHandler() {
    setCollapsed(!collapsed);
  }

  return (
    <Layout>
      <Sider
        trigger={null}
        collapsible
        collapsed={collapsed}
        style={{ minHeight: "100vh" }}
      >
        <div className={styles.logo}></div>
        <Menu theme="dark" mode="inline">
          <Menu.Item key="mall-list">
            <Link to="/mall-info-list">AVM Listesi</Link>
          </Menu.Item>
          <Menu.Item key="brand-list">
            <Link to="/brand-list">Marka Listesi</Link>
          </Menu.Item>
          <Menu.Item key="store-list">
            <Link to="/store-list">Mağaza Listesi</Link>
          </Menu.Item>
          <Menu.Item key="category-list">
            <Link to="/category-list">Kategori Listesi</Link>
          </Menu.Item>
          <Menu.Item key="snapshot-list">
            <Link to="/snapshot-list">Gözlem Listesi</Link>
          </Menu.Item>
          <Menu.Item key="user-list">
            <Link to="/user-list">Kullanıcı Listesi</Link>
          </Menu.Item>
        </Menu>
      </Sider>
      <Layout className={styles.layout}>
        <Header className={styles.siteLayoutBackground}>
          <Card bodyStyle={{ padding: "0", display: "flex" }}>
            {collapsed ? (
              <MenuUnfoldOutlined
                className={styles.trigger}
                onClick={toggleHandler}
              />
            ) : (
              <MenuFoldOutlined
                className={styles.trigger}
                onClick={toggleHandler}
              />
            )}
            <Card style={{ marginLeft: "auto", borderLeft: "none" }}>
              {authInfo.userFullName} <UserAvatar />
            </Card>
          </Card>
        </Header>
        <Content className={styles.siteLayoutBackground}>
          {props.children}
        </Content>
      </Layout>
    </Layout>
  );
}

export default MainLayout;
