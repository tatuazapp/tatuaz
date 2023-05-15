import { useAuth0 } from "@auth0/auth0-react"
import {
  Modal,
  ModalOverlay,
  ModalContent,
  ModalHeader,
  ModalCloseButton,
  ModalBody,
  ModalFooter,
  Button,
  useToast,
  FormControl,
  Textarea,
  VStack,
  Box,
} from "@chakra-ui/react"
import { X } from "@styled-icons/bootstrap/X"
import { useMutation } from "@tanstack/react-query"
import Image from "next/image"
import { FunctionComponent, useEffect, useMemo, useState } from "react"
import { useForm } from "react-hook-form"
import { FormattedMessage, useIntl } from "react-intl"
import { api } from "../../../../api/apiClient"
import { FinalizePostDto } from "../../../../api/tatuazApi"
import Dropzone from "../../../common/Dropzone"
import { UploadedPhotosWrapper } from "./styles"

type CreatePostModalProps = {
  isOpen: boolean
  onClose: () => void
}

const CreatePostModal: FunctionComponent<CreatePostModalProps> = ({
  isOpen,
  onClose,
}) => {
  const intl = useIntl()
  const toast = useToast()
  const {
    register,
    handleSubmit,
    setError,
    formState: { isSubmitting },
  } = useForm({
    defaultValues: {},
  })

  const [uploadedImages, setUploadedImages] = useState<File[] | null>(null)
  const [postId, setPostId] = useState<string>("")
  const [accessToken, setAccessToken] = useState<string>("")

  const { getAccessTokenSilently } = useAuth0()
  console.log("accessToken", accessToken)

  useEffect(() => {
    const getAccessToken = async () => {
      try {
        const accessToken = await getAccessTokenSilently()
        setAccessToken(accessToken)
      } catch (e) {
        console.log(e.message)
      }
    }
    getAccessToken()
  }, [getAccessTokenSilently])

  const uploadedImagesAsBase64: string[] = useMemo(() => {
    if (!uploadedImages) {
      return []
    }

    return uploadedImages.map((uploadedImage) =>
      URL.createObjectURL(uploadedImage)
    )
  }, [uploadedImages])

  const onFileAccepted = (file: File) => {
    setUploadedImages((uploadedImages) =>
      uploadedImages ? [...uploadedImages, file] : [file]
    )
  }

  const removeUploadedImage = (index: number) => () => {
    setUploadedImages((uploadedImages) =>
      uploadedImages ? uploadedImages.filter((_, i) => i !== index) : []
    )
  }

  console.log("uploadedImages", uploadedImages)

  const uploadPostPhotosMutation = useMutation({
    mutationFn: (data: { Photos?: File[] }) => api.post.uploadPostPhotos(data),
    onSuccess(data, variables, context) {
      setPostId(data.value.initialPostId)
      console.log(data)
    },
    onError: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Wystąpił błąd podczas zmiany avataru",
          id: "24ACHW",
        }),
        status: "error",
        position: "top",
      })
    },
  })

  const finalizePostMutation = useMutation({
    // mutationFn: (data: File[]) => api.post.finalizePost({ Photos: data }),
    mutationFn: (data: FinalizePostDto) => api.post.finalizePost(data),
    onError: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Wystąpił błąd podczas zmiany avataru",
          id: "24ACHW",
        }),
        status: "error",
        position: "top",
      })
    },
  })

  const getCategoriesMutation = useMutation({
    mutationFn: () =>
      api.photo.listCategories({
        pageNumber: 1,
        pageSize: 100,
      }),
    onSuccess: (data) => {
      console.log(data)
    },
  })

  const onSubmit = useMemo(
    () =>
      handleSubmit(async () => {
        if (!uploadedImages) {
          setError("backgroundPhoto", {
            message: intl.formatMessage({
              defaultMessage: "Avatar jest wymagany",
              id: "uwVZTo",
            }),
          })
          return
        }

        console.log("uploadedImages", uploadedImages)

        const formData = new FormData()
        for (let i = 0; i < uploadedImages.length; i++) {
          formData.append("Photos", uploadedImages[i])
        }

        const response = await fetch(
          "https://api.tatuaz.app/Post/UploadPostPhotos",
          {
            method: "POST",
            body: formData,
            headers: {
              "Content-Type": "multipart/form-data",
              Authorization:
                "Bearer " +
                "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCIsImtpZCI6Ing0dnBVTjRyX1hHOFMxbE0xQ2RrUiJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiIwMW5pa29kZW13QGdtYWlsLmNvbSIsImlzcyI6Imh0dHBzOi8vdGF0dWF6LWFwcC5ldS5hdXRoMC5jb20vIiwic3ViIjoiZ29vZ2xlLW9hdXRoMnwxMTI3NDM0OTUwNjU2OTQ5OTY3NDEiLCJhdWQiOlsiaHR0cHM6Ly90YXR1YXotYXBwLmV1LmF1dGgwLmNvbS9hcGkvdjIvIiwiaHR0cHM6Ly90YXR1YXotYXBwLmV1LmF1dGgwLmNvbS91c2VyaW5mbyJdLCJpYXQiOjE2ODQxNzc0NjcsImV4cCI6MTY4Njc2OTQ2NywiYXpwIjoiMU04bks3azdWQUs2N0d6dW1qS3NhMndndVpzZXlhZnkiLCJzY29wZSI6Im9wZW5pZCBwcm9maWxlIGVtYWlsIn0.E3yp5j0oal43syNeHM2Nl-QPmhN3SK55yB0v07imN9WXa0caXXoN94Q1FBBbUhFissZMDzi1fSuIsNHowvmsy2K3inGQx7O0aUTFPsO7y_Pea4FaejRFaxtEUzeCZd9pPRMhIacoYLLMt6Lcm-VpbzbdhWrCmfoMQDzX9U58dFhw-Fxq2njlF-c3DjM3WlxlFeohWJ8aIPVMk7KzPgifYYFn2aPCkEh8k9FQ65LKv6fNGESOqmn4PHOA7msILk0GQcfMGr1JCb7XpRsgVhn-FN0pfRPfYz1xWpiIuoIDVwIHwdUkQrXhvSiMNsYR_NZjRSa3WynPn8LvkMXFB9qPIg",
            },
          }
        )

        // console.log("response", response)

        // await uploadPostPhotosMutation.mutateAsync({
        //   Photos: uploadedImages,
        // })

        // const finalizePost: FinalizePostDto = {
        //   initialPostId: postId,
        //   description: "test",
        //   photoInfoDtos: [
        //     {
        //       photoFileName: "test",
        //       photoId: "test",
        //       categoryIds: [0],
        //     },
        //   ],
        // }

        // await finalizePostMutation.mutateAsync(finalizePost)

        onClose()
      }),
    [
      handleSubmit,
      intl,
      onClose,
      setError,
      uploadedImages,
      uploadPostPhotosMutation,
      postId,
      accessToken,
      // finalizePostMutation,
    ]
  )

  return (
    <Modal isOpen={isOpen} size="xl" onClose={onClose}>
      <ModalOverlay />
      <ModalContent>
        <ModalHeader>
          <FormattedMessage defaultMessage="Utwórz post" id="dAnIMm" />
        </ModalHeader>
        <ModalCloseButton />
        <ModalBody>
          <VStack align="stretch" spacing={4}>
            <FormControl>
              <Textarea
                placeholder="Co tam napiszesz?"
                {...register("post")}
                maxLength={4096}
              />
            </FormControl>
            <UploadedPhotosWrapper>
              {uploadedImagesAsBase64.map((uploadedImageAsBase64, index) => (
                <Box key={index} position="relative">
                  <Image
                    alt={intl.formatMessage({
                      defaultMessage: "Zdjęcie tła",
                      id: "cWa16k",
                    })}
                    height={0}
                    sizes="100vw"
                    src={uploadedImageAsBase64}
                    style={{ width: "100%", height: "auto" }} // optional
                    width={0}
                  />
                  <Button
                    bgColor="blackAlpha.600"
                    padding={0}
                    position="absolute"
                    right="0"
                    top="0"
                    onClick={removeUploadedImage(index)}
                  >
                    <X size="18" />
                  </Button>
                </Box>
              ))}
            </UploadedPhotosWrapper>

            {uploadedImagesAsBase64.length < 5 && (
              <Dropzone onFileAccepted={onFileAccepted} />
            )}
          </VStack>
        </ModalBody>

        <ModalFooter>
          <Button mr={3} variant="ghost" onClick={onClose}>
            <FormattedMessage defaultMessage="Anuluj" id="JmR+Nv" />
          </Button>
          <Button
            colorScheme="blue"
            isLoading={isSubmitting}
            onClick={onSubmit}
          >
            <FormattedMessage defaultMessage="Zapisz" id="Jt6oMg" />
          </Button>
        </ModalFooter>
      </ModalContent>
    </Modal>
  )
}

export default CreatePostModal
