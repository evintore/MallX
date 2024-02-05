import React, { useState } from "react";
import { Table, Card, Input } from "antd";
import { Link } from "react-router-dom";
import { PlusSquareOutlined } from "@ant-design/icons";
import { useQuery } from "react-query";
import api from "../../../api";
import { columns } from "./columns";
import useDebounce from "../../../commons/hooks/useDebounce";

function UserList() {
  const [searchKey, setSearchkey] = useDebounce("", 500);
  const [paginationData, setPaginationData] = useState({
    current: 1,
    pageSize: 10,
    position: "bottomCenter",
    total: 0,
  });

  const getUsersQuery = useQuery(
    [
      "getUsers",
      "user",
      paginationData.current,
      paginationData.pageSize,
      "",
      searchKey,
    ],
    api.getList
  );
  const users = getUsersQuery.data?.data;
  const totalCount = getUsersQuery.data?.totalCount;

  const handleTableChange = async (pagination) => {
    setPaginationData((current) => ({
      ...current,
      current: pagination.current,
      pageSize: pagination.pageSize,
    }));
  };

  const handleSearch = (e) => {
    setSearchkey(e.target.value);
  };

  return (
    <Card title="Kullanıcı Listesi" bordered={false}>
      <Card>
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
          }}
        >
          <div>
            <Input placeholder="Kullanıcı Ara.." onChange={handleSearch} />
          </div>
          <div>
            <Link to="/user-create">
              <PlusSquareOutlined
                style={{ fontSize: "1.7em", color: "#53acff" }}
              />
            </Link>
          </div>
        </div>
      </Card>
      <Table
        style={{ width: "100%" }}
        columns={columns}
        rowKey={(record) => record.pkid}
        dataSource={users}
        pagination={{
          ...paginationData,
          total: totalCount,
          showSizeChanger: true,
          pageSizeOptions: ["10", "20", "30"],
        }}
        loading={getUsersQuery.isLoading}
        onChange={handleTableChange}
        scroll={{ x: true }}
        showSizeChanger
        showQuickJumper
      />
    </Card>
  );
}

export default UserList;
