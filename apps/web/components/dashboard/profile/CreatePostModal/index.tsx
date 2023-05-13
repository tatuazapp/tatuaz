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
import { FunctionComponent, useMemo, useState } from "react"
import { useForm } from "react-hook-form"
import { FormattedMessage, useIntl } from "react-intl"
import { api } from "../../../../api/apiClient"
import { queryKeys } from "../../../../api/queryKeys"
import { SetBioDto } from "../../../../api/tatuazApi"
import { queryClient } from "../../../../pages/_app"
import {
  ErrorApiResponse,
  SetBioDtoErrorCode,
} from "../../../../types/apiErrors"
import ApiErrorHandler from "../../../../utils/errors/ApiErrorHandler"
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
  } = useForm<SetBioDto>({
    defaultValues: {},
  })

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

  const mutation = useMutation({
    mutationFn: (data: SetBioDto) => api.identity.setBio(data),
    onError: (res: ErrorApiResponse) => {
      const handler = new ApiErrorHandler<SetBioDtoErrorCode>(res.error)

      handler
        .handle("BioTooLong", () => {
          setError("bio", {
            type: "manual",
            message: intl.formatMessage({
              defaultMessage: "Bio jest za długie",
              id: "Q+mZoQ",
            }),
          })
        })
        .handle("CityTooLong", () => {
          setError("city", {
            type: "manual",
            message: intl.formatMessage({
              defaultMessage: "Nazwa miasta jest za długa",
              id: "cPjild",
            }),
          })
        })
        .handle("*", () => {
          toast({
            title: intl.formatMessage({
              defaultMessage:
                "Wystąpił błąd podczas aktualizacji. Spróbuj ponownie później",
              id: "qP/bHU",
            }),
            status: "error",
            position: "top",
          })
        })
        .run()
    },
    onSuccess: () => {
      toast({
        title: intl.formatMessage({
          defaultMessage: "Bio i Miasto zostały zaktualizowane",
          id: "QwvT2h",
        }),
        status: "success",
        position: "top",
      })
      queryClient.invalidateQueries({
        queryKey: [queryKeys.whoAmI],
      })
      onClose()
    },
  })

  const onSubmit = handleSubmit((data) => {
    mutation.mutateAsync(data)
  })

  const [uploadedImages, setUploadedImages] = useState<File[] | null>(null)

  const uploadedImagesAsBase64: string[] = useMemo(() => {
    if (!uploadedImages) {
      return []
    }

    return uploadedImages.map((uploadedImage) =>
      URL.createObjectURL(uploadedImage)
    )
  }, [uploadedImages])

  console.log("uploadedImagesAsBase64 ", uploadedImagesAsBase64)

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
