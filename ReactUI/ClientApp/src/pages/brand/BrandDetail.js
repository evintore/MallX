import React from "react";
import { useHistory, useParams } from "react-router-dom";
import { Form, Input, Button, notification, Select } from "antd";
import { Card, Col } from "antd";
import { useQuery, useMutation, useQueryClient } from "react-query";
import api from "../../api";

function BrandDetail() {
  let history = useHistory();
  const [form] = Form.useForm();
  const { id } = useParams();
  const { Option } = Select;
  const title = id ? "Marka Düzenle" : "Marka Ekle";

  const categoryId = Form.useWatch("categoryId", form);

  const Qbrand = useQuery(["getBrand", "brand", id], api.getOneById, {
    enabled: id !== undefined,
    staleTime: Infinity,
  });
  const brandInitial = Qbrand.data?.data;

  const QgetCategoriesQuery = useQuery(
    ["getCategories", "category"],
    api.getList,
    {
      staleTime: Infinity,
    }
  );
  const categories = QgetCategoriesQuery.data?.data ?? [];

  const QgetSubcategoriesQuery = useQuery(
    [
      "getSubcategories",
      "subcatergory",
      undefined,
      undefined,
      undefined,
      categoryId === "" ? undefined : categoryId,
    ],
    api.getList,
    {
      enabled: categoryId !== undefined,
      staleTime: Infinity,
    }
  );
  const subcategories = QgetSubcategoriesQuery.data?.data ?? [];

  const queryClient = useQueryClient();
  const saveBrand = useMutation(api.save, {
    onSuccess: (res) => {
      const description = id
        ? "Kayıt başarıyla güncellendi"
        : "Kayıt başarıyla eklendi";
      if (res.status === 201 || res.status === 204) {
        notification["success"]({
          message: "Başarılı",
          description: description,
        });
        history.push("/brand-list");
      }
      queryClient.invalidateQueries("saveBrand");
    },
  });

  const onFinish = async (values) => {
    const method = id ? "PUT" : "POST";
    const customValues = id ? { ...values, pkId: Number(id) } : values;
    saveBrand.mutate({ query: "brand", data: customValues, method });
  };

  const handleClick = () => {
    history.push("/brand-list");
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

  if (id && !brandInitial) {
    return null;
  }

  return (
    <Col span={18}>
      <Card title={title} bordered={false}>
        <Form
          {...formItemLayout}
          form={form}
          initialValues={brandInitial}
          name="brand_detail"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item
            name="brandName"
            label="Marka İsmi"
            rules={[
              {
                required: true,
                message: "Lütfen marka ismini giriniz!",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="categoryId"
            label="Kategori İsmi"
            rules={[
              {
                required: true,
                message: "Lütfen kategori ismini giriniz!",
              },
            ]}
          >
            <Select placeholder="Kategori Seç">
              {categories.map((category) => (
                <Option value={category.pkId}>{category.categoryName}</Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            name="subCategoryId"
            label="Alt Kategori"
            rules={[
              {
                required: true,
                message: "Lütfen alt kategoriyi giriniz!",
              },
            ]}
          >
            <Select
              placeholder="Alt kategori Seç"
              disabled={!form.getFieldValue("categoryId")}
            >
              {subcategories.map((subcategory) => (
                <Option value={subcategory.pkId}>
                  {subcategory.subcategoryName}
                </Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item {...tailFormItemLayout}>
            <Button type="seconder" onClick={handleClick}>
              Geri
            </Button>
            <Button type="primary" htmlType="submit">
              {id ? "Güncelle" : "Ekle"}
            </Button>
          </Form.Item>
        </Form>
      </Card>
    </Col>
  );
}

export default BrandDetail;
