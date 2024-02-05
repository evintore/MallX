import React, { useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { Form, Input, Select, Button, notification } from "antd";
import { Card, Col } from "antd";
import { useQuery, useMutation, useQueryClient } from "react-query";
import api from "../../api";
import SubcategoryForCategory from "./common/SubcategoryForCategory";

function CategoryDetail() {
  let history = useHistory();
  const [form] = Form.useForm();
  const { id } = useParams();
  const [categoryId, setCategoryId] = useState(id);
  const { Option } = Select;
  const title = id ? "Kategori Düzenle" : "Kategori Ekle";
  const [isViewSubcategoryTable, setIsViewSubcategoryTable] = useState(false);

  const Qcategory = useQuery(["getCategory", "category", id], api.getOneById, {
    enabled: id !== undefined,
    staleTime: Infinity,
  });
  const categoryInitial = Qcategory.data?.data;

  // const QgetSubcategoriesQuery = useQuery(
  //   ["getSubcategories", "subcategory"],
  //   api.getList,
  //   {
  //     staleTime: Infinity,
  //   }
  // );
  // const subcategories = QgetSubcategoriesQuery.data?.data ?? [];

  const queryClient = useQueryClient();
  const saveCategory = useMutation(api.save, {
    onSuccess: (res) => {
      const description = id
        ? "Kayıt başarıyla güncellendi"
        : "Kayıt başarıyla eklendi";
      if (res.status === 201 || res.status === 204) {
        notification["success"]({
          message: "Başarılı",
          description: description,
        });
        setIsViewSubcategoryTable(true);
      }
      queryClient.invalidateQueries("saveCategory");

      res.json().then((apiRes) => setCategoryId(apiRes.data.pkId));
    },
  });

  const onFinish = async (values) => {
    const method = id ? "PUT" : "POST";
    const customValues = id ? { ...values, pkId: Number(id) } : values;
    saveCategory.mutate({ query: "category", data: customValues, method });
  };
  const handleClick = () => {
    history.push("/category-list");
  };

  const formItemLayout = {
    labelCol: {
      xs: {
        span: 24,
      },
      sm: {
        span: 8,
      },
    },
    wrapperCol: {
      xs: {
        span: 24,
      },
      sm: {
        span: 16,
      },
    },
  };
  const tailFormItemLayout = {
    wrapperCol: {
      xs: {
        span: 24,
        offset: 0,
      },
      sm: {
        span: 16,
        offset: 8,
      },
    },
  };

  if (id && !categoryInitial) {
    return null;
  }

  return (
    <Col span={18}>
      <Card title={title} bordered={false}>
        <Form
          {...formItemLayout}
          form={form}
          initialValues={categoryInitial}
          name="category_detail"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item
            name="categoryName"
            label="Kategori İsmi"
            rules={[
              {
                required: true,
                message: "Lütfen kategori ismini giriniz!",
              },
            ]}
          >
            <Input style={{ width: 160 }} />
          </Form.Item>
          <Form.Item {...tailFormItemLayout}>
            <Button type="seconder" onClick={handleClick}>
              Geri
            </Button>
            <Button type="primary" htmlType="submit">
              {id ? "Güncelle" : "Kaydet"}
            </Button>
          </Form.Item>
        </Form>
        {(id || isViewSubcategoryTable) && (
          <SubcategoryForCategory categoryId={categoryId} />
        )}
      </Card>
    </Col>
  );
}

export default CategoryDetail;
