import React, { useState } from "react";
import { Table, Card, Input } from "antd";
import { Link } from "react-router-dom";
import { PlusSquareOutlined } from "@ant-design/icons";
import { convertToOrderBy } from "../../../utils/index";
import { useQuery } from "react-query";
import api from "../../../api";
import { columns } from "./columns";
import useDebounce from "../../../commons/hooks/useDebounce";

function CategoryList() {
  const [sorter, setSorter] = useState();
  const [searchKey, setSearchkey] = useDebounce("", 500);
  const [paginationData, setPaginationData] = useState({
    current: 1,
    pageSize: 10,
    position: "bottomCenter",
  });

  const getCategoriesQuery = useQuery(
    [
      "getCategories",
      "category",
      paginationData.current,
      paginationData.pageSize,
      sorter ? convertToOrderBy(sorter.field, sorter.order) : "",
      searchKey,
    ],
    api.getList
  );
  const categories = getCategoriesQuery.data?.data;
  const totalCount = getCategoriesQuery.data?.totalCount;

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
    <Card title="Kategori Listesi" bordered={false}>
      <Card>
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
          }}
        >
          <div>
            <Input placeholder="Kategori Ara.." onChange={handleSearch} />
          </div>
          <div>
            <Link to="/category-create">
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
        dataSource={categories}
        pagination={{
          ...paginationData,
          total: totalCount,
          showSizeChanger: true,
          pageSizeOptions: ["10", "20", "30"],
        }}
        loading={getCategoriesQuery.isLoading}
        onChange={handleTableChange}
        scroll={{ x: true }}
        showSizeChanger
        showQuickJumper
      />
    </Card>
  );
}

export default CategoryList;
