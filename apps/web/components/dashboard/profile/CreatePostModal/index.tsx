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
  FormLabel,
  FormErrorMessage,
} from "@chakra-ui/react"
import { X } from "@styled-icons/bootstrap/X"
import { useMutation, useQuery } from "@tanstack/react-query"
import { Props, Select } from "chakra-react-select"
import Image from "next/image"
import { FunctionComponent, useMemo, useState } from "react"
import { Controller, useForm } from "react-hook-form"
import { FormattedMessage, useIntl } from "react-intl"
import { api } from "../../../../api/apiClient"
import useMe from "../../../../api/hooks/useMe"
import { queryKeys } from "../../../../api/queryKeys"
import { FinalizePostDto } from "../../../../api/tatuazApi"
import { queryClient } from "../../../../pages/_app"
import Dropzone from "../../../common/Dropzone"
import { UploadedPhotosWrapper } from "./styles"

type CreatePostModalProps = {
  isOpen: boolean
  onClose: () => void
}

type PostFormValues = {
  description: string
  photos: File[]
  categories?: {
    value: number
    label: string
  }[]
}

const CreatePostModal: FunctionComponent<CreatePostModalProps> = ({
  isOpen,
  onClose,
}) => {
  const intl = useIntl()
  const toast = useToast()
  const {
    control,
    register,
    handleSubmit,
    reset,
    formState: { errors },
  } = useForm<PostFormValues>({
    defaultValues: {},
  })

  const me = useMe()
  const [uploadedImages, setUploadedImages] = useState<File[] | null>(null)

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

  const uploadPostPhotosMutation = useMutation({
    mutationFn: (data: { Photos?: File[] }) => api.post.uploadPostPhotos(data),
    onError: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Wystąpił błąd podczas wgrywania zdjęć",
          id: "hPM1O9",
        }),
        status: "error",
        position: "top",
      })
    },
  })

  const finalizePostMutation = useMutation({
    mutationFn: (data: FinalizePostDto) => api.post.finalizePost(data),
    onSuccess: () =>
      queryClient.invalidateQueries({
        queryKey: [queryKeys.getUserPosts, me?.username ?? ""],
      }),
    onError: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Wystąpił błąd podczas tworzenia posta",
          id: "eSTVQL",
        }),
        status: "error",
        position: "top",
      })
    },
  })

  const { data: photoCategories } = useQuery(
    [queryKeys.photo.listCategories],
    () =>
      api.photo.listCategories({
        pageNumber: 1,
        pageSize: 100, // pagination is not necessary here
      }),
    {
      refetchOnWindowFocus: false,
    }
  )

  const categoriesOptions = useMemo(
    () =>
      photoCategories?.value?.data.map((category) => ({
        label: category.title,
        value: category.id,
      })) satisfies Props["options"],
    [photoCategories]
  )

  const onSubmit = useMemo(
    () =>
      handleSubmit(async (data) => {
        const resUpload = await uploadPostPhotosMutation.mutateAsync({
          Photos: uploadedImages ?? [],
        })

        const initialPostId = resUpload.value.initialPostId

        const finalizePost: FinalizePostDto = {
          initialPostId,
          description: data.description,
          photoInfoDtos: resUpload.value.photos.map((photoId) => ({
            photoId: photoId,
            categoryIds:
              data.categories?.map((category) => category.value) ?? [],
            photoFileName: `${Math.random().toString(36).substring(2, 15)}`,
          })),
        }

        await finalizePostMutation.mutateAsync(finalizePost)

        reset({
          description: "",
          photos: [],
          categories: [],
        })
        setUploadedImages(null)

        onClose()
      }),
    [
      handleSubmit,
      uploadPostPhotosMutation,
      uploadedImages,
      finalizePostMutation,
      reset,
      onClose,
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
            <FormControl isInvalid={!!errors.description}>
              <Textarea
                id="description"
                placeholder={intl.formatMessage({
                  defaultMessage: "Co tam napiszesz?",
                  id: "3b3RDG",
                })}
                {...register("description", {
                  required: intl.formatMessage({
                    defaultMessage: "Treść posta jest wymagana",
                    id: "FKsikb",
                  }),
                })}
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
                    style={{ width: "100%", height: "auto" }}
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
            {uploadedImagesAsBase64.length > 0 && (
              <Controller
                control={control}
                name="categories"
                render={({
                  field: { onChange, onBlur, value, name, ref },
                  fieldState: { error },
                }) => (
                  <FormControl id="food" isInvalid={!!error} py={4}>
                    <FormLabel>
                      <FormattedMessage
                        defaultMessage="Kategorie zdjęć"
                        id="qV9U4T"
                      />
                    </FormLabel>
                    <Select
                      ref={ref}
                      isMulti
                      name={name}
                      options={categoriesOptions ?? []}
                      placeholder={intl.formatMessage({
                        defaultMessage: "Wybierz kategorie zdjęć",
                        id: "Gc10Un",
                      })}
                      value={value}
                      onBlur={onBlur}
                      onChange={onChange}
                    />

                    <FormErrorMessage>
                      {error && error.message}
                    </FormErrorMessage>
                  </FormControl>
                )}
              />
            )}
          </VStack>
        </ModalBody>

        <ModalFooter>
          <Button mr={3} variant="ghost" onClick={onClose}>
            <FormattedMessage defaultMessage="Anuluj" id="JmR+Nv" />
          </Button>
          <Button
            colorScheme="primary"
            isLoading={
              uploadPostPhotosMutation.isLoading ||
              finalizePostMutation.isLoading
            }
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
