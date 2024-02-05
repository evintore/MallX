import React from "react";
import { useHistory, useParams } from "react-router-dom";
import { Form, Input, Button, Card, Col, notification } from "antd";
import { useQuery, useMutation, useQueryClient } from "react-query";
import api from "../../api";

const UserDetail = () => {
  let history = useHistory();
  const [form] = Form.useForm();
  const { id } = useParams();
  const title = id ? "Kullanıcı Düzenle" : "Kullanıcı Ekle";

  const Quser = useQuery(["getUser", "user", id], api.getOneById, {
    enabled: id !== undefined,
    staleTime: Infinity,
  });
  const userInitial = Quser.data?.data;

  const queryClient = useQueryClient();
  const saveUser = useMutation(api.save, {
    onSuccess: (res) => {
      const description = id
        ? "Kayıt başarıyla güncellendi"
        : "Kayıt başarıyla eklendi";
      if (res.status === 201 || res.status === 204) {
        notification["success"]({
          message: "Başarılı",
          description: description,
        });
        history.push("/user-list");
      }
      queryClient.invalidateQueries("saveUser");
    },
  });

  const onFinish = async (values) => {
    const method = id ? "PUT" : "POST";
    const customValues = id ? { ...values, pkId: Number(id) } : values;
    saveUser.mutate({ query: "user", data: customValues, method });
  };

  const handleClick = () => {
    history.push("/user-list");
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

  if (id && !userInitial) {
    return null;
  }

  return (
    <Col span={18}>
      <Card title={title} bordered={false}>
        <Form
          {...formItemLayout}
          form={form}
          initialValues={userInitial}
          name="user_detail"
          onFinish={onFinish}
          scrollToFirstError
        >
          <Form.Item
            name="fullName"
            label="Kullanıcı"
            rules={[
              {
                required: true,
                message: "Lütfen kullanıcı ismini giriniz!!",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="mail"
            label="E posta"
            rules={[
              {
                required: true,
                message: "Lütfen e posta adresini giriniz!",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            name="password"
            label="Şifre"
            rules={[
              {
                required: true,
                message: "Lütfen şifreyi giriniz!!",
              },
            ]}
          >
            <Input.Password />
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
};

export default UserDetail;
