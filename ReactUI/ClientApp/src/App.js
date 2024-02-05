import React from "react";
import "antd/dist/antd.css";
import { Switch, Route } from "react-router-dom";
import MainLayout from "./pages/Layout";

import "./custom.css";
import MallInfoList from "./pages/mallInfo/list/MallInfoList";
import MallInfoDetail from "./pages/mallInfo/MallInfoDetail";

import UserList from "./pages/User/list/UserList";
import UserDetail from "./pages/User/UserDetail";
import Authenticator from "./components/Authenticator/index.authenticator";

import StoreList from "./pages/store/list/StoreList";
import StoreDetail from "./pages/store/StoreDetail";

import BrandList from "./pages/brand/list/BrandList";
import BrandDetail from "./pages/brand/BrandDetail";

import SnapshotList from "./pages/snapshot/list/SnapshotList";
import SnapshotDetail from "./pages/snapshot/SnapshotDetail";

import CategoryList from "./pages/category/list/CategoryList";
import CategoryDetail from "./pages/category/CategoryDetail";

import {
  // useQuery,
  // useMutation,
  // useQueryClient,
  QueryClient,
  QueryClientProvider,
} from "react-query";

export const AuthContext = React.createContext({});
const queryClient = new QueryClient();

function App() {
  return (
    <Authenticator>
      {(authInfo) => {
        return (
          <AuthContext.Provider value={authInfo}>
            <QueryClientProvider client={queryClient}>
              <Switch>
                <MainLayout>
                  <Route path="/mall-info-create">
                    <MallInfoDetail />
                  </Route>
                  <Route path="/mall-info-list">
                    <MallInfoList />
                  </Route>
                  <Route path="/mall-info/:id">
                    <MallInfoDetail />
                  </Route>
                  <Route path="/user-list">
                    <UserList />
                  </Route>
                  <Route path="/user/:id">
                    <UserDetail />
                  </Route>
                  <Route path="/user-create">
                    <UserDetail />
                  </Route>
                  <Route path="/store-list">
                    <StoreList />
                  </Route>
                  <Route path="/store/:id">
                    <StoreDetail />
                  </Route>
                  <Route path="/store-create">
                    <StoreDetail />
                  </Route>
                  <Route path="/brand-list">
                    <BrandList />
                  </Route>
                  <Route path="/brand/:id">
                    <BrandDetail />
                  </Route>
                  <Route path="/brand-create">
                    <BrandDetail />
                  </Route>
                  <Route path="/snapshot-list">
                    <SnapshotList />
                  </Route>
                  <Route path="/snapshot/:id">
                    <SnapshotDetail />
                  </Route>
                  <Route path="/snapshot-create">
                    <SnapshotDetail />
                  </Route>
                  <Route path="/category-list">
                    <CategoryList />
                  </Route>
                  <Route path="/category/:id">
                    <CategoryDetail />
                  </Route>
                  <Route path="/category-create">
                    <CategoryDetail />
                  </Route>
                </MainLayout>
              </Switch>
            </QueryClientProvider>
          </AuthContext.Provider>
        );
      }}
    </Authenticator>
  );
}

export default App;
