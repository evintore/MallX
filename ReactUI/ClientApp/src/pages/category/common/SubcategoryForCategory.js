import React, { useEffect, useState } from "react";
import { Table, Card, Input, Button, Form } from "antd";
import { convertToOrderBy } from "../../../utils/index";
import api from "../../../api";
import subcategoryColumn from "./Columns";
import useDebounce from "../../../commons/hooks/useDebounce";
import EditableCell from "../../../components/dataTable/EditableCell";
import { useQuery, useMutation, useQueryClient } from "react-query";

const SubCategoryForCategory = (props) => {
  const { categoryId } = props;
  const [sorter, setSorter] = useState();
  const [data, setData] = useState([]);
  const [searchKey, setSearchkey] = useDebounce("", 500);
  const [form] = Form.useForm();
  const [paginationData, setPaginationData] = useState({
    current: 1,
    pageSize: 10,
    position: "bottomCenter",
  });
  const [editingKey, setEditingKey] = useState("");

  console.log("cateroyId", categoryId);
  const getSubcategoriesQuery = useQuery(
    [
      "getSubcategories",
      "subcategory",
      paginationData.current,
      paginationData.pageSize,
      sorter ? convertToOrderBy(sorter.field, sorter.order) : "",
      searchKey,
      categoryId,
    ],
    api.getSubcategoryList
  );
  const subcategories = getSubcategoriesQuery.data?.data;
  const totalCount = getSubcategoriesQuery.data?.totalCount;

  const saveSubcategory = useMutation(api.save, {
    onSuccess: (res) => {
      res.json().then((apiRes) => {
        setData((currentData) =>
          currentData.map((item) =>
            item.pkId === 0 ? { ...item, pkId: apiRes.data.pkId } : item
          )
        );
      });
    },
  });

  useEffect(() => {
    console.log("subcategories", subcategories);
    if (subcategories) {
      setData([...subcategories]);
    }
  }, [subcategories]);

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
  const handleAdd = () => {
    const newData = {
      pkId: 0,
      subcategoryName: "",
      categoryId,
    };
    setData([...data, newData]);
    setEditingKey(data.length);
  };

  const handleDBSave = (item) => {
    //.. save to db
    console.log("handle db save", item);

    saveSubcategory.mutate({
      query: "subcategory",
      data: item,
      method: item.pkId > 0 ? "PUT" : "POST",
    });
  };

  return (
    <Card title="Alt Kategori Listesi" bordered={false}>
      <Card>
        <div
          style={{
            display: "flex",
            justifyContent: "space-between",
          }}
        >
          <div>
            <Input placeholder="Alt Kategori Ara.." onChange={handleSearch} />
          </div>
          <div>
            <Button
              onClick={handleAdd}
              type="primary"
              style={{ marginBottom: 16 }}
            >
              Ekle
            </Button>
          </div>
        </div>
      </Card>
      {data && (
        <Form form={form} component={false}>
          <Table
            style={{ width: "100%" }}
            rowKey={(record) => record.pkid}
            columns={subcategoryColumn(
              data,
              setData,
              setEditingKey,
              editingKey,
              form,
              handleDBSave
            )}
            dataSource={data}
            pagination={{
              ...paginationData,
              total: totalCount,
              showSizeChanger: true,
              pageSizeOptions: ["10", "20", "30"],
            }}
            components={{
              body: {
                cell: EditableCell,
              },
            }}
            rowClassName="editable-row"
            loading={getSubcategoriesQuery.isLoading}
            onChange={handleTableChange}
            scroll={{ x: true }}
            showSizeChanger
            showQuickJumper
          />
        </Form>
      )}
    </Card>
  );
};

export default SubCategoryForCategory;
