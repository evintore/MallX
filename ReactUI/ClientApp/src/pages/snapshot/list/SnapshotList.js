import React, { useEffect, useState } from "react";
import { Table, Card, Input } from "antd";
import { Link } from "react-router-dom";
import { PlusSquareOutlined } from "@ant-design/icons";
import { convertToOrderBy } from "../../../utils/index";
import { useQuery } from "react-query";
import api from "../../../api";
import { columns } from "./columns";
import useDebounce from "../../../commons/hooks/useDebounce";

function SnapshotList() {
  const [sorter, setSorter] = useState();
  const [searchKey, setSearchkey] = useDebounce("", 500);
  const [paginationData, setPaginationData] = useState({
    current: 1,
    pageSize: 10,
    position: "bottomCenter",
  });

  const getSnapshotsQuery = useQuery(
    [
      "getSnapshots",
      "snapshot",
      paginationData.current,
      paginationData.pageSize,
      sorter ? convertToOrderBy(sorter.field, sorter.order) : "",
      searchKey,
    ],
    api.getList
  );

  const snapshots = getSnapshotsQuery.data?.data;
  const totalCount = getSnapshotsQuery.data?.totalCount;

  const handleTableChange = async (pagination, filters, sorter) => {
    setSorter(sorter);
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
    <Card title="Gözlem Listesi" bordered={false}>
      <Card>
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
          }}
        >
          <div>
            <Input placeholder="Mağaza veya AVM Ara.." onChange={handleSearch} />
          </div>
          <div>
            <Link to="/snapshot-create">
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
        dataSource={snapshots}
        pagination={{
          ...paginationData,
          total: totalCount,
          showSizeChanger: true,
          pageSizeOptions: ["10", "20", "30"],
        }}
        loading={getSnapshotsQuery.isLoading}
        onChange={handleTableChange}
        scroll={{ x: true }}
        showSizeChanger
        showQuickJumper
      />
    </Card>
  );
}

export default SnapshotList;
