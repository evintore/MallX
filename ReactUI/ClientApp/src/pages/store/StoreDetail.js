import React, { useEffect, useState } from "react";
import { useHistory, useParams } from "react-router-dom";
import { Form, Input, Select, Button, notification } from "antd";
import { Card, Col } from "antd";
import { useQuery, useMutation, useQueryClient } from "react-query";
import api from "../../api";

function StoreDetail() {
  let history = useHistory();
  const [mallInfoName, setMallInfoName] = useState();
  const [brandName, setBrandName] = useState();
  const [form] = Form.useForm();
  const { id } = useParams();
  const { Option } = Select;
  const title = id ? "Mağaza Düzenle" : "Mağaza Ekle";

  const Qstore = useQuery(["getStore", "store", id], api.getOneById, {
    enabled: id !== undefined,
    staleTime: Infinity,
  });
  const storeInitial = Qstore.data?.data;

  const QgetBrandsQuery = useQuery(["getBrands", "brand"], api.getList, {
    staleTime: Infinity,
  });
  const brands = QgetBrandsQuery.data?.data ?? [];

  const QgetMallInfosQuery = useQuery(
    ["getMallInfos", "mallInfo"],
    api.getList,
    {
      staleTime: Infinity,
    }
  );
  const mallInfos = QgetMallInfosQuery.data?.data ?? [];

  const queryClient = useQueryClient();
  const saveStore = useMutation(api.save, {
    onSuccess: (res) => {
      const description = id
        ? "Kayıt başarıyla güncellendi"
        : "Kayıt başarıyla eklendi";
      if (res.status === 201 || res.status === 204) {
        notification["success"]({
          message: "Başarılı",
          description: description,
        });
        history.push("/store-list");
      }
      queryClient.invalidateQueries("saveStore");
    },
  });

  const onFinish = async (values) => {
    const method = id ? "PUT" : "POST";
    const customValues = id ? { ...values, pkId: Number(id) } : values;
    saveStore.mutate({ query: "store", data: customValues, method });
  };
  const handleClick = () => {
    history.push("/store-list");
  };
  const mallInfoChangeHandler = (value, info) => {
    setMallInfoName(info.children);
  };
  const brandChangeHandler = (value, info) => {
    setBrandName(info.children);
  };

  useEffect(() => {
    if (mallInfoName && brandName) {
      form.setFieldsValue({ storeName: `${mallInfoName} - ${brandName}` });
    }
  }, [mallInfoName, brandName]);

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

  if (id && !storeInitial) {
    return null;
  }

  return (
    <Col span={18}>
      <Card title={title} bordered={false}>
        <Form
          {...formItemLayout}
          form={form}
          initialValues={storeInitial}
          name="store_detail"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item
            name="mallInfoId"
            label="Avm İsmi"
            rules={[
              {
                required: true,
                message: "Lütfen AVM ismi giriniz!!",
              },
            ]}
          >
            <Select placeholder="AVM seç" onChange={mallInfoChangeHandler}>
              {mallInfos.map((mall) => (
                <Option value={mall.pkId}>{mall.mallName}</Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            name="brandId"
            label="Marka İsmi"
            rules={[
              {
                required: true,
                message: "Lütfen marka ismi giriniz!!",
              },
            ]}
          >
            <Select placeholder="Marka seç" onChange={brandChangeHandler}>
              {brands.map((brand) => (
                <Option value={brand.pkId}>{brand.brandName}</Option>
              ))}
            </Select>
          </Form.Item>
          <Form.Item
            name="storeName"
            label="Mağaza İsmi"
            rules={[
              {
                required: true,
                message: "Lütfen mağaza ismini giriniz!",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="floor"
            label="Kat Numarası"
            rules={[
              {
                required: true,
                message: "Lütfen kat numarasını giriniz!!",
              },
            ]}
          >
            <Input />
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

export default StoreDetail;
